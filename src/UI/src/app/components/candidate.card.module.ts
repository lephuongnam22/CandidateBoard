import { ChangeDetectionStrategy, Component, Input,Injector, Inject, Output, EventEmitter } from '@angular/core';
import {PolymorpheusComponent} from '@tinkoff/ng-polymorpheus';
import {TuiCardModule, TuiHeaderModule, TuiSurfaceModule, TuiTitleModule, TuiChipModule} from '@taiga-ui/experimental';
import {CandidateModel, JobModel} from './models';
import {TuiDialogService, TuiLabelModule} from '@taiga-ui/core';
import { CandidateDialogComponent } from './dialogs';
import { CommonModule, NgFor } from '@angular/common';

@Component({
    selector: 'candidate-card',
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    styleUrls: ['./candidate.card.less'],
    template: `
    
        <button
            tuiCardLarge
            tuiSurface="elevated"
            (click)="onClick()"
        >
           <table class="tui-table">
            <tbody>
                <tr>
                    <td>Full Name:</td>
                    <td><strong>{{candidate.firstName}} {{candidate.lastName}}</strong></td>
                </tr>
                <tr>
                    <td>Phone Number: </td>
                    <td>{{candidate.phoneNumber}}</td>
                </tr>
                <tr>
                    <td>Email Address:</td>
                    <td>{{candidate.email}}</td>
                </tr>
                <tr>
                    <td>Create Date:</td>
                    <td>{{candidate.createDate}}</td>
                </tr>
                <tr>
                    <td>Job Applied:</td>
                    <td>
                        <tui-chip *ngFor="let job of candidate.jobModels" [style.border-radius.rem]="5">{{job.jobTitle}}</tui-chip>   
                    </td>
                </tr>
                <tr>
                    <td>Interviewer:</td>
                    <td>
                        <tui-chip *ngFor="let interviewer of candidate.interviewerModels"
                        [style.border-radius.rem]="5">{{interviewer.name}}</tui-chip>   
                    </td>
                </tr>
            </tbody>
                        
        </table>
                
           
        </button>
    
    `,
    imports: [
        TuiCardModule,
        TuiSurfaceModule,
        TuiTitleModule,
        TuiHeaderModule,
        CommonModule,
        NgFor,
        TuiLabelModule,
        TuiChipModule
        // ...
      ],
  })

  export class CandidateCardModule {
    @Input() candidate: CandidateModel;
    @Output() onAddCandidate = new EventEmitter<any>();


    constructor(@Inject(TuiDialogService) private readonly dialogService: TuiDialogService,
    @Inject(Injector) private readonly injector: Injector,) {}

    protected onClick() {
        let dialogEditCandidate = this.dialogService.open<CandidateModel>(
            new PolymorpheusComponent(CandidateDialogComponent, this.injector),
            { dismissible: true, label: 'Edit Candidate', data: {
                firstName: this.candidate.firstName,
                lastName: this.candidate.lastName,
                email: this.candidate.email,
                phoneNumber: this.candidate.phoneNumber,
                jobModels: this.candidate.jobModels,
                interviewerModels: this.candidate.interviewerModels
            } }
        );

        dialogEditCandidate.subscribe({
            next: data => {
              if(data && data.id != 0) {
                this.onAddCandidate.emit(data);
              }
            }
        });
    }
  }