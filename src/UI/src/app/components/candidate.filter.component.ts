import { ChangeDetectionStrategy, Component, Injector, Inject, Output, EventEmitter, OnInit} from '@angular/core';
import { FormGroup, FormControl,FormsModule,ReactiveFormsModule } from '@angular/forms';
import {TuiDay, TuiDayRange} from '@taiga-ui/cdk';
import {TuiInputDateRangeModule, TuiMultiSelectModule, TuiInputModule, TuiDataListWrapperModule, TuiInputDateModule} from '@taiga-ui/kit';
import {TuiContextWithImplicit, TuiStringHandler} from '@taiga-ui/cdk';
import {TuiButtonModule, TuiDialogService, TuiDataListModule} from '@taiga-ui/core';
import {InterviewerModel } from './models';
import { CommonModule } from '@angular/common';
import {TuiIconModule} from '@taiga-ui/experimental';
import {PolymorpheusComponent} from '@tinkoff/ng-polymorpheus';
import { CandidateDialogComponent } from './dialogs';
import {AddCandidateResultModel} from './models';
import { InterviewerService } from './services';

@Component({
    standalone: true,
    selector: 'candidate-filter',
    changeDetection: ChangeDetectionStrategy.OnPush,
    styles:[],
    templateUrl:'./candidate.filter.component.html',
    styleUrl:'./candidate.filter.component.less',
    imports: [
      CommonModule,
      TuiInputDateRangeModule,
      FormsModule,
      ReactiveFormsModule,
      TuiMultiSelectModule,
      TuiInputModule,
      TuiIconModule,
      TuiButtonModule,
      TuiDataListWrapperModule,
      TuiDataListModule,
      TuiInputDateModule

      ],
      providers: [InterviewerService]
})

export class CandidateFilterComponent implements OnInit {

  constructor(@Inject(TuiDialogService) private readonly dialogService: TuiDialogService,
    @Inject(Injector) private readonly injector: Injector,
    private readonly interviewerService: InterviewerService) {}

  interviewers: InterviewerModel[];

  @Output() onAddCandidate = new EventEmitter<any>();
  @Output() onSearchCandidate = new EventEmitter<any>();

  ngOnInit() {

    this.interviewerService.getInterviewers().subscribe(res => {
      if(res) {
        this.interviewers = new Array<InterviewerModel>();

        res.forEach(n => {
          this.interviewers.push({id: n.id, name: n.name});
        });
      }
    });
  }

  private readonly dialogAddCandidate = this.dialogService.open<AddCandidateResultModel>(
    new PolymorpheusComponent(CandidateDialogComponent, this.injector),
    { dismissible: true, label: 'Add Candidate' }
  );

  filterForm = new FormGroup({
      candidateName: new FormControl(''),
      appliedFromDate: new FormControl(new TuiDay(2024, 1, 1)),
      appliedToDate: new FormControl(new TuiDay(2024, 1, 1)),
      interviewerValue: new FormControl<any[] | null>(null),
      },
  );

  readonly stringifyInterviewer: TuiStringHandler<InterviewerModel | TuiContextWithImplicit<InterviewerModel>> = interviewer =>
      'name' in interviewer ? interviewer.name : interviewer.$implicit.name;

  submit() {
    if(this.filterForm.controls.appliedFromDate.value && this.filterForm.controls.appliedToDate.value) {
      if(this.filterForm.controls.appliedFromDate.value > this.filterForm.controls.appliedToDate.value) {
        this.dialogService.open(`
        <div>
          Applied From Date cannot greater than Applied To Date.
        </div>`, {label: 'Error', size: 's'})
        .subscribe();

        return;
      }
    }

  
    var searchRequest = {
      candidateName: this.filterForm.controls.candidateName.value,
      fromDate: this.filterForm.controls.appliedFromDate.value,
      toDate: this.filterForm.controls.appliedToDate.value,
      interviewerIds: this.filterForm.controls.interviewerValue.value 
      ? this.filterForm.controls.interviewerValue.value.map(n => n.id) : []
    }; 

    this.onSearchCandidate.emit(searchRequest);
  }

  onAddCandidateClick() {
    this.dialogAddCandidate.subscribe({
      next: data => {
        if(data && data.candidate && data.candidate.id != 0) {
          this.onAddCandidate.emit(data.candidate);
        }
      }
  });
  }
}