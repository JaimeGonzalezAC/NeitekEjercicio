using System.Net.Http.Json;

namespace RF_ToDoBlazor.Services
{
    public class TodoApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress = "http://localhost:5056/api/";

        public TodoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseAddress);
        }

        #region Task
        public async Task<IEnumerable<Task>> GetTasksByGoalAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Task>>($"Tasks/{id}/byGoal");
        }
        public async Task<IEnumerable<Task>> GetTasksAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Task>>("Tasks");
        }
        public async Task<Task> GetTaskAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Task>($"Tasks/{id}");
        }
        public async Task<Task> CreateTaskAsync(Task task)
        {
            var response = await _httpClient.PostAsJsonAsync("Tasks", task);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Task>();
        }
        public async System.Threading.Tasks.Task UpdateTaskAsync(Guid id, Task task)
        {
            var response = await _httpClient.PutAsJsonAsync($"Tasks/{id}", task);
            response.EnsureSuccessStatusCode();
        }
        public async System.Threading.Tasks.Task CompleteTask(Guid id)
        {
            var response = await _httpClient.PutAsync($"Tasks/{id}/Completed", null);
            response.EnsureSuccessStatusCode();
        }
        public async System.Threading.Tasks.Task MarkTaskAsImportant(Guid id)
        {
            var response = await _httpClient.PutAsync($"Tasks/{id}/isImpornat", null);
            response.EnsureSuccessStatusCode();
        }
        public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"Tasks/{id}");
            response.EnsureSuccessStatusCode();
        }

        #endregion

        #region Goals
        // All Goals with task info
        public async Task<IEnumerable<GoalWithTaks>> GetGoalsWithTaskAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<GoalWithTaks>>("Goals/withtask");
        }

        // All Goals
        public async Task<IEnumerable<Goal>> GetGoalsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Goal>>("Goals");
        }

        // Goal by ID
        public async Task<Goal> GetGoalAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Goal>($"Goals/{id}");
        }

        // Create (POST)
        public async Task<Goal> CreateGoalAsync(Goal goal)
        {
            var response = await _httpClient.PostAsJsonAsync("Goals", goal);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Goal>();
        }

        // Update (PUT)
        public async System.Threading.Tasks.Task UpdateGoalAsync(Guid id, Goal goal)
        {
            var response = await _httpClient.PutAsJsonAsync($"Goals/{id}", goal);
            response.EnsureSuccessStatusCode();
        }

        // Delete (DELETE)
        public async System.Threading.Tasks.Task DeleteGoalAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"Goals/{id}");
            response.EnsureSuccessStatusCode();
        }
        #endregion
    }

    public class Task
    {
        public Guid TaskId { get; set; }
        public Guid GoalId { get; set; }
        public string Name { get; set; }
        public bool isImportant { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
    }

    public class Goal
    {
        public Guid GoalId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
    public class GoalWithTaks
    {
        public Guid GoalId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
        public double CompletionPercentage { get; set; }
    }
}
