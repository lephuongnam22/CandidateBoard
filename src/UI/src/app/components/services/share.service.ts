import { Injectable } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { CandidateModel } from '../models';


@Injectable({
    providedIn: 'root',
  })
export class ShareService {

    public drop(event: CdkDragDrop<CandidateModel[]>) {
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