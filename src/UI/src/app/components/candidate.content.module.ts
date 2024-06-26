import { ChangeDetectionStrategy, Component, Input, OnInit, ChangeDetectorRef, Inject,Injector  } from '@angular/core';
import { CandidateCardModule, CandidateModel } from './index';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import {TuiSurfaceModule, TuiCardModule, TuiTitleModule} from '@taiga-ui/experimental';
import { DragDropModule,CdkDragDrop, moveItemInArray,
    transferArrayItem, 
    CdkDropListGroup, 
    CdkDropList, CdkDrag } from '@angular/cdk/drag-drop';

import {CandidateService} from './services/candidate.service';
import { delay, forkJoin, Observable } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { CandidateStautusModel, SearchRequest } from './models';
import {TuiLoaderModule, TuiDialogService} from '@taiga-ui/core';
import {Subject} from 'rxjs';

@Component({
    selector: 'candidate-content',
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    styleUrls: ['./candidate.content.module.less'],
    templateUrl:'./candidate.content.module.html',
    imports: [
        CandidateCardModule,
        NgFor,
        NgIf,
        CommonModule,
        TuiSurfaceModule,
        TuiCardModule,
        DragDropModule,
        CdkDropListGroup,
        CdkDropList,
        CdkDrag,
        TuiTitleModule,
        HttpClientModule,
        TuiLoaderModule,
      ],
      providers: [CandidateService]
})

export class CandidateContentModule implements OnInit {

    constructor(private readonly candidateService: CandidateService,
      private changeDetection: ChangeDetectorRef,
      @Inject(TuiDialogService) private readonly dialogService: TuiDialogService,
      @Inject(Injector) private readonly injector: Injector,
    ) {
        
    }

    @Input() candidateStatus: Array<CandidateStautusModel>;
    readonly loading$ = new Subject<boolean>();
    noData$ = new Subject<boolean>;
    
    ngOnInit() {
      
      let searchRequest: SearchRequest = {
        candidateName: "",
        interviewerIds:[],
        appliedFromDate: "",
        appliedToDate: ""
      };

      this.loadCandidate(searchRequest);
    }

    onAddNewCandidate(candidate: CandidateModel): void {
    var applidedList = this.candidateStatus.find(n => n.status == "Applied");

      if(applidedList) {
        if(!applidedList.candidates) {
          applidedList.candidates = [];
        }

        applidedList.candidates = [...applidedList.candidates.concat(candidate)];
        this.changeDetection.detectChanges();
      }
    }

    onSearchCandidate(searchRequest: SearchRequest): void {
      this.loadCandidate(searchRequest);
    }

    onEditCandidate(candidate: CandidateModel): void {
      this.candidateStatus.forEach(element => {
        var oldCandidate = element.candidates?.find(n => n.id == candidate.id);

        if(oldCandidate) {
          oldCandidate.firstName = candidate.firstName;
          oldCandidate.lastName = candidate.lastName;
          oldCandidate.email = candidate.email;
          oldCandidate.phoneNumber = candidate.phoneNumber;
          oldCandidate.jobModels = candidate.jobModels;
          oldCandidate.interviewerModels = candidate.interviewerModels;
          this.changeDetection.detectChanges();
        }
      });
    }

    loadCandidate(searchRequest: SearchRequest) {
      this.loading$.next(true);
      this.candidateStatus = [];
      this.candidateService.searchCandidate(searchRequest).subscribe(res => {
        if(res) {

          res.forEach(n => {
            let dropStatus = res.map(n => n.candidateStatus).filter(x => x != n);
            this.candidateStatus.push({status: n.candidateStatus, dropStatus: dropStatus, candidates: n.candidates});
          });

          this.noData$.next(false);
          this.loading$.next(false);
        }
      },(error) => {
        this.noData$.next(true);
      }
      );
    }

    drop(event: CdkDragDrop<any[]>) {
        if (event.previousContainer === event.container) {
            // Reorder items within the same list
             moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
        } else {
         
          this.loading$.next(true);
          let candidate = event.item.data;
          candidate.CandidateStatus = event.container.id;

          transferArrayItem(
            event.previousContainer.data,
            event.container.data,
            event.previousIndex,
            event.currentIndex
          );

          this.candidateService.updateCandidateStatus(candidate).pipe(delay(1000)).subscribe(res => {

            if(res) {
              this.loading$.next(false);
            }
          },(error) => {

            transferArrayItem(
              event.container.data,
              event.previousContainer.data,
              event.currentIndex,
              event.previousIndex,
            );

            this.dialogService.open(`
            <div>
              Cannot update Candiate Status. Please contact Administrator.
            </div>`, {label: 'Error', size: 's'})
            .subscribe();
            this.loading$.next(false);
          });
        }
    }

 
}

