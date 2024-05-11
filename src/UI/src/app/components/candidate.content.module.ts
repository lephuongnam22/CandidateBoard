import { ChangeDetectionStrategy, Component, Input, OnInit, ChangeDetectorRef, Inject,Injector  } from '@angular/core';
import { CandidateCardModule, CandidateModel } from './index';
import { CommonModule, NgFor } from '@angular/common';
import {TuiSurfaceModule, TuiCardModule, TuiTitleModule} from '@taiga-ui/experimental';
import { DragDropModule,CdkDragDrop, moveItemInArray,
    transferArrayItem, 
    CdkDropListGroup, 
    CdkDropList, CdkDrag } from '@angular/cdk/drag-drop';

import {CandidateService} from './services/candidate.service';
import { delay, finalize, forkJoin } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { CandidateStautusModel } from './models';
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
    statuses: string[];
    readonly loading$ = new Subject<boolean>();
    
    ngOnInit() {
      this.loading$.next(true);
        forkJoin(this.candidateService.getcandidateStatus(), this.candidateService.getCandidates()).subscribe(res => {
        this.candidateStatus = [];
        this.statuses = [];
        
              if(res[0]) {
                let status = res[0];
                this.statuses = res[0];
                res[0].forEach(n => 
                  {
                    let dropStatus = status.filter(a => a != n);

                    var candidateItem = res[1].find(x => x.candidateStatus === n);
                    let candidates = [];
                    if(candidateItem) {
                      candidates = candidateItem.candidates;
                    }

                    this.candidateStatus.push({status: n, candidates: candidates, dropStatus: dropStatus});
                  });
              }

              this.loading$.next(false);
              //this.changeDetection.detectChanges();
            },(error) => {
              console.log(error);
              this.loading$.next(false);

              this.dialogService.open(`
              <div>
                Cannot load data from API Server. Please contact Administrator.
              </div>`, {label: 'Error', size: 's'})
              .subscribe();
              
              this.changeDetection.detectChanges();
            });
          
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

    drop(event: CdkDragDrop<any[]>) {
        if (event.previousContainer === event.container) {
            // Reorder items within the same list
             moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
        } else {
         
          this.loading$.next(true);
          let candidate = event.item.data;
          candidate.CandidateStatus = event.container.id;

          this.candidateService.updateCandidateStatus(candidate).pipe(delay(1000)).subscribe(res => {

            if(res) {
              transferArrayItem(
                event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex
              );

              this.loading$.next(false);
            }
          },(error) => {
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

