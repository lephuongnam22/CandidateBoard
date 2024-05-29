
import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {TuiDialogContext, TuiErrorModule, TuiDataListModule} from '@taiga-ui/core';
import {TuiInputModule, TuiFieldErrorPipeModule, TuiDataListWrapperModule, TuiSelectModule, TuiMultiSelectModule} from '@taiga-ui/kit';
import { TuiButtonModule } from '@taiga-ui/experimental';
import { FormControl, FormsModule, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import {POLYMORPHEUS_CONTEXT} from '@tinkoff/ng-polymorpheus';
import { CommonModule } from '@angular/common';
import { JobService, CandidateService, InterviewerService } from '../../services/';
import { HttpClientModule } from '@angular/common/http';
import {  JobModel, InterviewerModel, AddCandidateResultModel } from '../../models';
import {TuiContextWithImplicit, TuiStringHandler, TuiIdentityMatcher} from '@taiga-ui/cdk';
import { forkJoin } from 'rxjs';

@Component({
    selector: 'candidate-dialog',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    templateUrl: './candidate.dialog.template.html',
    styleUrls: ['./candidate.dialog.style.less'],
    imports:[
        TuiInputModule,
        TuiButtonModule,
        FormsModule,
        TuiErrorModule,
        ReactiveFormsModule,
        TuiFieldErrorPipeModule,
        CommonModule,
        TuiDataListModule,
        TuiDataListWrapperModule,
        TuiSelectModule,
        HttpClientModule,
        TuiMultiSelectModule,
    ],
    providers: [JobService, HttpClientModule, CandidateService, InterviewerService],
})

export class CandidateDialogComponent implements OnInit {
    jobs: JobModel[];
    interviewers: InterviewerModel[];
    private oldEmail: string;

    constructor(
        @Inject(POLYMORPHEUS_CONTEXT)
        private readonly context: TuiDialogContext<AddCandidateResultModel, AddCandidateResultModel>,
        private readonly jobService: JobService,
        private readonly candidateService: CandidateService,
        private readonly interviewerService: InterviewerService
      ) {}

    candidateForm = new FormGroup({
        firstNameValue: new FormControl('', Validators.required),
        lastNameValue: new FormControl('', Validators.required),
        phoneValue: new FormControl('', Validators.required),
        emailValue: new FormControl('', Validators.compose([Validators.required, Validators.email])),
        jobValue: new FormControl<JobModel[] | null>(null, Validators.required),
        interviewerValue: new FormControl<InterviewerModel[] | null>(null, Validators.required),
      });

    readonly stringifyJob: TuiStringHandler<JobModel | TuiContextWithImplicit<JobModel>> = job =>
      'jobTitle' in job ? job.jobTitle : job.$implicit.jobTitle;

    readonly identityMatcherJob: TuiIdentityMatcher<JobModel> = (job1, job2) =>
      job1.id === job2.id;

    readonly stringifyInterviewer: TuiStringHandler<InterviewerModel | TuiContextWithImplicit<InterviewerModel>> = interviewer =>
      'name' in interviewer ? interviewer.name : interviewer.$implicit.name;

    readonly identityMatcherInterviewer: TuiIdentityMatcher<InterviewerModel> = (Interviewer1, Interviewer2) =>
      Interviewer1.id === Interviewer2.id;

    ngOnInit() {
      forkJoin(this.jobService.getJobs(), this.interviewerService.getInterviewers()).subscribe(res => {
        this.jobs = new Array<JobModel>;
        this.interviewers = new Array<InterviewerModel>();

        if(res[0] && res[0].length > 0) {
          res[0].forEach(n => {
            this.jobs.push({id: n.id, jobTitle: n.jobTitle, jobDescription: n.jobDescription});
          });
        }

        if(res[1] && res[1].length > 0) {
          res[1].forEach(n => {
            this.interviewers.push({id: n.id, name: n.name});
          });
        }

        this.updateDisplay();
      });
    }

    updateDisplay() {
      if(this.context.data) {
        this.oldEmail = this.context.data.candidate.email;
        this.candidateForm.controls.firstNameValue.setValue(this.context.data.candidate.firstName);
        this.candidateForm.controls.lastNameValue.setValue(this.context.data.candidate.lastName);
        this.candidateForm.controls.emailValue.setValue(this.context.data.candidate.email);
        this.candidateForm.controls.phoneValue.setValue(this.context.data.candidate.phoneNumber);
        
        if(this.context.data.candidate.jobModels && this.context.data.candidate.jobModels.length > 0) {
          let selectedJobs: Array<JobModel> = [];
          this.context.data.candidate.jobModels.forEach(j => {
            var job = this.jobs.find(n => n.id === j.id);

            if(job) {
              selectedJobs.push(job);
            }
          });

          this.candidateForm.controls.jobValue.setValue(selectedJobs);
        }
        if(this.context.data.candidate.interviewerModels && this.context.data.candidate.interviewerModels.length > 0) {
          let selectedInterviewers: Array<InterviewerModel> = [];

          this.context.data.candidate.interviewerModels.forEach(j => {
            var interviewer = this.interviewers.find(n => n.id === j.id);

            if(interviewer) {
              selectedInterviewers.push(interviewer);
            }
          });

          this.candidateForm.controls.interviewerValue.setValue(selectedInterviewers);
        }
      }
    }

    submit() {
      this.candidateForm.markAllAsTouched();

      if(this.candidateForm.valid) {
        let candidate:any;
        candidate = {
          id: 0,
          firstName: this.candidateForm.controls.firstNameValue.value,
          lastName: this.candidateForm.controls.lastNameValue.value,
          phoneNumber: this.candidateForm.controls.phoneValue.value,
          email: this.candidateForm.controls.emailValue.value,
          jobIds: this.candidateForm.controls.jobValue.value?.map(n => n.id),
          interviewerIds: this.candidateForm.controls.interviewerValue.value?.map(n => n.id),
          candidateStatus: "Applied",
          oldEmail: this.oldEmail
        };

        if(this.context.data && !this.context.data.isAdd) {
          candidate.id = this.context.data.candidate.id;

          this.candidateService.updateCandidate(candidate).subscribe(res => {
            if(res) {
              let data: AddCandidateResultModel = {isAdd: false, candidate: res };
              this.context.completeWith(data);
            }
          });
        }
        else {
          this.candidateService.addcandidate(candidate).subscribe(res => {
            if(res) {
              let data: AddCandidateResultModel = {isAdd: true, candidate: res };
              this.context.completeWith(data);
            }
          });
        }
      }
      
    } 

    cancel() {
      this.context.completeWith({isAdd: false, candidate: {id: 0, firstName: "", lastName: "", phoneNumber: "", email: "", jobModels:[], interviewerModels: [], createDate:""} });
    }
}