
import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {TuiDialogContext, TuiErrorModule, TuiDataListModule} from '@taiga-ui/core';
import {TuiInputModule, TuiFieldErrorPipeModule, TuiDataListWrapperModule, TuiSelectModule} from '@taiga-ui/kit';
import { TuiButtonModule } from '@taiga-ui/experimental';
import { FormControl, FormsModule, Validators, FormGroup, ReactiveFormsModule, NgForm } from '@angular/forms';
import {POLYMORPHEUS_CONTEXT} from '@tinkoff/ng-polymorpheus';
import { CommonModule } from '@angular/common';
import { Job } from '../../models/job.model';
import { JobService } from '../../services/job.service';
import { HttpClientModule } from '@angular/common/http';
import { CandidateService } from '../../services/candidate.service';
import {  CandidateModel } from '../../models';

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
        HttpClientModule 
    ],
    providers: [JobService, HttpClientModule, CandidateService]
})

export class CandidateDialogComponent implements OnInit {

    jobs: Array<Job>;

    constructor(
        @Inject(POLYMORPHEUS_CONTEXT)
        private readonly context: TuiDialogContext<void, CandidateModel>,
        private readonly jobService: JobService,
        private readonly candidateService: CandidateService
      ) {}

    candidateForm = new FormGroup({
        firstNameValue: new FormControl('', Validators.required),
        lastNameValue: new FormControl('', Validators.required),
        phoneValue: new FormControl('', Validators.required),
        emailValue: new FormControl('', Validators.required),
        jobValue: new FormControl<Job | null>(null, Validators.required)
      });

    ngOnInit() {
      this.jobService.getJobs().subscribe(res => {
        this.jobs = [];
        if(res && res.length > 0) {
          res.forEach(n => {
            let job = new Job(n.id, n.jobTitle, n.jobDescription);
            this.jobs.push(job);
          });
        }

        if(this.context.data) {
          this.candidateForm.controls.firstNameValue.setValue(this.context.data.firstName);
          this.candidateForm.controls.lastNameValue.setValue(this.context.data.lastName);
          this.candidateForm.controls.emailValue.setValue(this.context.data.email);
          this.candidateForm.controls.phoneValue.setValue(this.context.data.phoneNumber);
          var job = this.jobs.find(n => n.id === this.context.data.jobId);
          //this.candidateForm.controls.jobValue.setValue(this.context.data.jobId);
  
          if(job) {
            this.candidateForm.controls.jobValue.setValue(job);
          }
         
        }
      });

      
    }

    submit() {
      this.candidateForm.markAllAsTouched();
      if(this.candidateForm.valid) {
        let candidate:any;
        candidate = {
          id: 3,
          firstName: this.candidateForm.controls.firstNameValue.value,
          lastName: this.candidateForm.controls.lastNameValue.value,
          phoneNumber: this.candidateForm.controls.phoneValue.value,
          email: this.candidateForm.controls.emailValue.value,
          jobId: this.candidateForm.controls.jobValue.value?.id,
          candidateStatus: "Applied",
        };


        this.candidateService.addcandidate(candidate).subscribe(res => {
          if(res) {
            this.context.completeWith(res);
          }
        });
      }
      
    } 

    cancel() {
      this.context.completeWith();
    }
}