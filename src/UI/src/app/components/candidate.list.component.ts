import { ChangeDetectionStrategy, Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import {CandidateModel} from './models';
import { CommonModule, NgFor } from '@angular/common';
import { CandidateCardModule } from './candidate.card.module';
import { ShareService } from './services/share.service';

import { DragDropModule,CdkDragDrop,moveItemInArray,
    transferArrayItem, 
    CdkDropListGroup, 
    CdkDropList, CdkDrag } from '@angular/cdk/drag-drop';

@Component({
    selector: 'candidate-list',
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush,
    styleUrls: ['./candidate.list.less'],
    template:`
        <div class="drop-list" cdkDropList id={{statusId}}
        [cdkDropListData]="candidateList" (cdkDropListDropped)="onDrop($event)"
        [cdkDropListConnectedTo]="dropStatus">
            @for (candidate of candidateList; track candidate) {
                <candidate-card cdkDrag [candidate]="candidate" [cdkDragData]="candidate"></candidate-card>
                
            }
            
        </div>
    `,
    imports: [
        CommonModule,
        NgFor,
        CandidateCardModule,
        DragDropModule,
        CdkDropListGroup,
        CdkDropList,
        CdkDrag,
      ],
      providers: [ShareService]
})

export class CandidateListComponent implements OnChanges {
    @Input() candidateList: CandidateModel[];
    @Input() statusId: string;
    @Input() statuses: string[];

    dropStatus: string[];

    constructor(private ss: ShareService) {

    }

    ngOnChanges(changes: SimpleChanges): void {
        if(changes['statuses'].firstChange) {
            this.dropStatus = this.statuses.filter(n => n != this.statusId);
        }
    }

    onDrop(event: CdkDragDrop<CandidateModel[]>) {
        if (event.previousContainer === event.container) {
            moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
          } else {
            transferArrayItem(event.previousContainer.data,
              event.container.data,
              event.previousIndex,
              event.currentIndex);
          }
  
          console.log(event.container.data);
          console.log(event.previousContainer.data);
    }

}