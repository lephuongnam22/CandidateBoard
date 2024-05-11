import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    // declares that this service should be created
    // by the root application injector.
    providedIn: 'root',
  })
export class InterviewerService {

    apiUrl = `${environment.baseUrl}/interviewers`;
    constructor(private http: HttpClient) { }

    getInterviewers(): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}`);
    }

}