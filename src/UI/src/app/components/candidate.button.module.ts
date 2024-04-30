import { ChangeDetectionStrategy, Component, Inject, Injector, Output, EventEmitter } from '@angular/core';
import {TuiButtonModule, TuiDialogService, TuiDialogContext} from '@taiga-ui/core';
import {PolymorpheusComponent} from '@tinkoff/ng-polymorpheus';
import { CandidateDialogComponent } from './dialogs';
import {CandidateModel} from './models';

@Component({
    selector: 'candidate-button',
    changeDetection: ChangeDetectionStrategy.Default,
    standalone: true,
    template: `
    <div>
        <button
            tuiButton
            type="button"
            class="tui-space_right-3 tui-space_bottom-3"
            (click)="onAddCandidateClick()"
        >
            Add Candidate
        </button>
    </div>
    `,
    styles:[`
    button {
      float:right;
      margin-right: 20px;
    }
    `]
    ,
    imports: [
        TuiButtonModule,
        CandidateDialogComponent
      ]
})

export class CandidateButtonModule {
    private readonly dialogAddCandidate = this.dialogService.open<CandidateModel>(
        new PolymorpheusComponent(CandidateDialogComponent, this.injector),
        { dismissible: true, label: 'Add Candidate' }
      );

    constructor(@Inject(TuiDialogService) private readonly dialogService: TuiDialogService,
    @Inject(Injector) private readonly injector: Injector,) {}

    @Output() onAddCandidate = new EventEmitter<any>();

    onAddCandidateClick(): void {
        this.dialogAddCandidate.subscribe({
            next: data => {
              if(data && data.id != 0) {
                this.onAddCandidate.emit(data);
              }
            }
        });
    }
}