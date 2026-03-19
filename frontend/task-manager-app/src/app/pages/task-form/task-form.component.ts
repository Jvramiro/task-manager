import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TaskCreateDTO, TaskPriority, TaskStatus } from "../../models/task.model";
import { TaskService } from "../../services/task.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: 'app-task-form',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './task-form.component.html',
    styleUrl: './task-form.component.css'
})
export class TaskFormComponent implements OnInit {
    isEdit = false;
    taskId: number | null = null;

    priorities = Object.values(TaskPriority);
    statuses = Object.values(TaskStatus);

    model: TaskCreateDTO = {
        title: '',
        description: '',
        priority: TaskPriority.Normal,
        status: TaskStatus.NotStarted
    };

    constructor(private taskService: TaskService, private router: Router, private route: ActivatedRoute) {}

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');
        if(id) {
            this.isEdit = true;
            this.taskId = +id;
            this.taskService.getById(this.taskId).subscribe(task => {
                this.model = {
                    title: task.title,
                    description: task.description,
                    priority: task.priority,
                    status: task.status
                };
            });
        }
    }

    save(): void {
        if(this.isEdit && this.taskId) {
            this.taskService.update(this.taskId, this.model).subscribe(() => {
                this.router.navigate(['/tasks']);
            });
        }
    }

    cancel(): void{
        this.router.navigate(['/tasks']);
    }

}