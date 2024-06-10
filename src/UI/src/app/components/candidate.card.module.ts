import { ChangeDetectionStrategy, Component, Input,Injector, Inject, Output, EventEmitter, output } from '@angular/core';
import {PolymorpheusComponent} from '@tinkoff/ng-polymorpheus';
import {TuiCardModule, TuiHeaderModule, TuiSurfaceModule, TuiTitleModule, TuiChipModule, TuiCellModule} from '@taiga-ui/experimental';
import {AddCandidateResultModel, CandidateModel, JobModel} from './models';
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
            <header tuiHeader>
                <h2 tuiTitle>{{candidate.firstName}} {{candidate.lastName}}</h2>
            </header>
            <div tuiCell>
                <div tuiTitle>Phone: 
                <div tuiSubTitle>{{candidate.phoneNumber}}</div>
                </div>
                
            </div>
            <div tuiCell>
                <div tuiTitle>Email: 
                    <div tuiSubTitle>{{candidate.email}}</div>
                </div>
            </div>
            <div tuiCell>
                <div tuiTitle>Applied Date:
                <div tuiSubTitle>{{candidate.createDate}}</div>
                </div>
                
            </div>
            <div tuiCell>
                <div tuiTitle>Applied Position:
                        <div> <tui-chip *ngFor="let job of candidate.jobModels" [style.border-radius.rem]="5">{{job.jobTitle}}</tui-chip></div>
                    </div>
                </div>
            
            <div tuiCell>
                <div tuiTitle>Interviewer:
                    <div>
                        <tui-chip *ngFor="let interviewer of candidate.interviewerModels"
                        [style.border-radius.rem]="5">{{interviewer.name}}</tui-chip>
                    </div>
                </div>
            </div>
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
        TuiChipModule,
        TuiCellModule
        // ...
      ],
  })

  export class CandidateCardModule {
    @Input() candidate: CandidateModel;
    @Output() onEditCandidate = new EventEmitter<any>();

    constructor(@Inject(TuiDialogService) private readonly dialogService: TuiDialogService,
    @Inject(Injector) private readonly injector: Injector,) {}

    protected onClick() {
        let dialogEditCandidate = this.dialogService.open<AddCandidateResultModel>(
            new PolymorpheusComponent(CandidateDialogComponent, this.injector),
            { dismissible: true, label: 'Edit Candidate', data: {
                isAdd: false,
                candidate: {
                    id: this.candidate.id,
                    firstName: this.candidate.firstName,
                    lastName: this.candidate.lastName,
                    email: this.candidate.email,
                    phoneNumber: this.candidate.phoneNumber,
                    jobModels: this.candidate.jobModels,
                    interviewerModels: this.candidate.interviewerModels
                }
            } }
        );

        dialogEditCandidate.subscribe({
            next: data => {
              if(data && data.candidate && data.candidate.id != 0) {
                if(!data.isAdd) {
                    this.onEditCandidate.emit(data.candidate);
                }
              }
            }
        });
    }
  }