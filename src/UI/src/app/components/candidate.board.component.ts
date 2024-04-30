import {Component, ViewChild} from '@angular/core';
import { CandidateContentModule, CandidateButtonModule,  CandidateTitleModule } from './index';
import {CandidateService} from './services/candidate.service';
import { CandidateModel } from './models';


@Component({
    standalone: true,
    selector: 'candidate-board',
    templateUrl: './candidate.board.component.html',
    styleUrls: ['./candidate.board.component.less'],
    imports: [
      CandidateContentModule,
      CandidateButtonModule,
      CandidateTitleModule,
    ],
    providers: [CandidateService]
  })

export class CandidateBoardComponent {

  constructor() {

  }

  @ViewChild(CandidateContentModule) candidateContent : CandidateContentModule;

  onAddCandidate($data: CandidateModel) {
    this.candidateContent.onAddNewCandidate($data);
  }
}