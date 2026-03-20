import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Task, TaskDTO } from "../models/task.model";

@Injectable({
    providedIn: 'root'
})
export class TaskService {
    private apiUrl = `${environment.apiUrl}/task`;

    constructor(private http: HttpClient) {}

    getAll(): Observable<Task[]> {
        return this.http.get<Task[]>(this.apiUrl);
    }

    getById(id: number): Observable<Task> {
        return this.http.get<Task>(`${this.apiUrl}/${id}`);
    }

    create(task: TaskDTO): Observable<Task> {
        return this.http.post<Task>(this.apiUrl, task);
    }

    update(id: number, task: TaskDTO): Observable<Task> {
        return this.http.put<Task>(`${this.apiUrl}/${id}`, task);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}