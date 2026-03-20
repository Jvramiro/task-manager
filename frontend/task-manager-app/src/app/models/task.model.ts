import { TaskPriority, TaskStatus } from "../enums/task-enums";

export interface Task {
  id: number;
  title: string;
  description: string;
  priority: TaskPriority;
  status: TaskStatus;
  createdAt: string;
}

export interface TaskCreateDTO {
  title: string;
  description: string;
  priority: TaskPriority;
  status: TaskStatus;
}