export enum TaskPriority {
  Low = 'Low',
  Normal = 'Normal',
  High = 'High'
}

export enum TaskStatus {
  NotStarted = 'NotStarted',
  InProgress = 'InProgress',
  Completed = 'Completed'
}

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