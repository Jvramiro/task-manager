using TaskManager.API.Enums;

namespace TaskManager.API.DTO;
public record TaskUpdateDTO(
    string Title,
    string Description,
    Priority Priority,
    Status Status
);