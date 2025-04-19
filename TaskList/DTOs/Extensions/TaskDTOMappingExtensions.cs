using Task = TaskList.Model.Task;

namespace TaskList.DTOs.Extensions
{
    public static class TaskDTOMappingExtensions
    {
        public static TaskDTO ToTaskDTO(this Task task)
        {
            if (task is null)
            {
                return null;
            }

            return new TaskDTO()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Date = task.Date,
                Status = task.Status,
                UserId = task.UserId,
            };
        }

        public static Task ToTask(this TaskDTO taskDTO)
        {
            if (taskDTO is null)
            {
                return null;
            }

            return new Task()
            {
                Id = taskDTO.Id,
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                Date = taskDTO.Date,
                Status = taskDTO.Status,
                UserId = taskDTO.UserId,
            };
        }

        public static IEnumerable<TaskDTO> ToTaskDTOList(this IEnumerable<Task> tasks)
        {
            if (tasks is null || !tasks.Any())
            {
                return new List<TaskDTO>();
            }

            return tasks.Select(task => new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Date = task.Date,
                Status = task.Status,
                UserId = task.UserId,
            }).ToList();
        }
    }
}
