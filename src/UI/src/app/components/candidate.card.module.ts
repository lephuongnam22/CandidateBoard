import { ChangeDetectionStrategy, Component, Input,Injector, Inject, Output, EventEmitter } from '@angular/core';
import {PolymorpheusComponent} from '@tinkoff/ng-polymorpheus';
import {TuiCardModule, TuiHeaderModule, TuiSurfaceModule, TuiTitleModule} from '@taiga-ui/experimental';
import {CandidateModel} from './models';
import {TuiDialogService} from '@taiga-ui/core';
import { CandidateDialogComponent } from './dialogs';

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
            <header>
                <div tuiTitle>
                    <strong>{{candidate.firstName}} {{candidate.lastName}}</strong>
                    <div tuiSubtitle>Phone Number: {{candidate.phoneNumber}}</div>
                    <div tuiSubtitle>Email: {{candidate.email}}</div>
                    <div tuiSubtitle>Job Applied: {{candidate.jobTitle}}</div>
                </div>
            </header>
        </button>
    
    `,
    imports: [
        TuiCardModule,
        TuiSurfaceModule,
        TuiTitleModule,
        TuiHeaderModule,
    
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
                jobId: this.candidate.jobId,
                jobTitle: this.candidate.jobTitle
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