import {Component} from '@angular/core';
import { CandidateContentModule, CandidateBoardComponent, CandidateFilterComponent } from './components/index';
import {TuiRootModule, TuiDialogModule, TuiButtonModule} from '@taiga-ui/core';
 
@Component({
  standalone: true,
  selector: 'app-root',
  styleUrls: ['./app.component.less'],
  templateUrl: './app.component.html',
  imports: [
    TuiRootModule,
    CandidateContentModule,
    CandidateFilterComponent,
    TuiButtonModule,
    TuiDialogModule,
    CandidateBoardComponent,
  ],
})
export class AppComponent {

  constructor(
  ) {}
}