import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
  })
export class CandidateService {
    apiUrl = `${environment.baseUrl}/candidates`;
    constructor(private http: HttpClient) { }

    getCandidates(): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}`);
    }

    addcandidate(candidate: any): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}`, candidate);
    }

    getCandidateById(id: number) : Observable<any> {
        return this.http.get<any>(`${this.apiUrl}/${id}`);
    }

    getcandidateStatus(): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}/candidate-status`);
    }

    updateCandidateStatus(candidate: any) : Observable<any> {
        return this.http.put<any>(`${this.apiUrl}/status`, candidate);
    } 
}