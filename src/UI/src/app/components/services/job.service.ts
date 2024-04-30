import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    // declares that this service should be created
    // by the root application injector.
    providedIn: 'root',
  })
export class JobService {

    apiUrl = `${environment.baseUrl}/jobs`;
    constructor(private http: HttpClient) { }

    getJobs(): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}`);
    }

    addJob(job: any): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}`, job);
    }
}