import { CommonModule } from "@angular/common";
import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TaskDTO } from "../../models/task.model";
import { TaskService } from "../../services/task.service";
import { ActivatedRoute, Router } from "@angular/router";
import { TaskPriority, TaskStatus } from "../../enums/task-enums";

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
    isLoading = false;

    priorities = Object.values(TaskPriority);
    statuses = Object.values(TaskStatus);

    model: TaskDTO = {
        title: '',
        description: '',
        priority: TaskPriority.Normal,
        status: TaskStatus.NotStarted
    };

    constructor(private taskService: TaskService, private router: Router, private route: ActivatedRoute, private cdr: ChangeDetectorRef) {}

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');
        if(id) {
            this.isEdit = true;
            this.taskId = +id;
            this.isLoading = true;
            this.taskService.getById(this.taskId).subscribe(task => {
                this.model = {
                    title: task.title,
                    description: task.description,
                    priority: task.priority,
                    status: task.status
                };
                this.isLoading = false;
                this.cdr.detectChanges();
            });
        }
    }

    save(): void {
        if(this.isEdit && this.taskId) {
            this.taskService.update(this.taskId, this.model).subscribe(() => {
                this.cdr.detectChanges();
                this.router.navigate(['/tasks']);
            });
        } else {
            this.taskService.create(this.model).subscribe(() => {
                this.cdr.detectChanges();
                this.router.navigate(['/tasks']);
            });
        }
    }

    cancel(): void{
        this.router.navigate(['/tasks']);
    }

}