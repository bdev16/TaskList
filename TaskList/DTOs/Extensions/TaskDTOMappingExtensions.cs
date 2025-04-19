using Task = TaskList.Model.Task;

namespace TaskList.DTOs.Extensions
{
    public static class TaskDTOMappingExtensions
    {
        public static TaskDTO ToTaskDTO(this Task task)
        {
            TaskDTO taskDTO = new TaskDTO()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Date = task.Date,
                Status = task.Status,
                UserId = task.UserId,
            };

            return taskDTO;
        }

        public static Task ToTask(this TaskDTO taskDTO)
        {
            Task task = new Task()
            {
                Id = taskDTO.Id,
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                Date = taskDTO.Date,
                Status = taskDTO.Status,
                UserId = taskDTO.UserId,
            };

            return task;
        }

        public static IEnumerable<TaskDTO> ToTaskDTOList(this IEnumerable<Task> tasks)
        {
            var tasksDTO = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                TaskDTO taskDTO = new TaskDTO()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Date = task.Date,
                    Status = task.Status,
                    UserId = task.UserId,
                };
                tasksDTO.Add(taskDTO);
            }

            return tasksDTO;
        }
    }
}
