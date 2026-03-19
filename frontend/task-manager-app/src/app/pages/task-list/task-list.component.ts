import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { Task } from "../../models/task.model";
import { TaskService } from "../../services/task.service";
import { Router } from "@angular/router";

@Component({
    selector: 'app-task-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './task-list.component.html',
    styleUrl: './task-list.component.css'
})
export class TaskListComponent implements OnInit {
    tasks: Task[] = [];

    constructor(private taskService: TaskService, private router: Router) {}

    ngOnInit(): void {
        this.loadTasks();
    }

    loadTasks(): void {
        this.taskService.getAll().subscribe(tasks => {
            this.tasks = tasks;
        });
    }

    newTask(): void {
        this.router.navigate(['/tasks/new']);
    }

    editTask(id: number): void {
        this.router.navigate(['/tasks/edit', id])
    }

    deleteTask(id: number): void {
        if(confirm('Do you want to delete this task?')) {
            this.taskService.delete(id).subscribe(() => {
                this.loadTasks();
            });
        }
    }
}