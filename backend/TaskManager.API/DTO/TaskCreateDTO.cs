using TaskManager.API.Enums;

namespace TaskManager.API.DTO;
public record TaskCreateDTO(
    string Title,
    string Description,
    Priority Priority,
    Status Status
);