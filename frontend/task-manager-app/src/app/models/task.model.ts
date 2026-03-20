import { TaskPriority, TaskStatus } from "../enums/task-enums";

export interface Task {
  id: number;
  title: string;
  description: string;
  priority: TaskPriority;
  status: TaskStatus;
  createdAt: string;
}

export interface TaskDTO {
  title: string;
  description: string;
  priority: TaskPriority;
  status: TaskStatus;
}