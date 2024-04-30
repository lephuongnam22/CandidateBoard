import {Component, Inject, Injector} from '@angular/core';
import { CandidateContentModule, CandidateTitleModule, CandidateButtonModule, CandidateBoardComponent } from './components/index';
import {TuiRootModule, TuiDialogModule, TuiButtonModule} from '@taiga-ui/core';
//import { TuiDialogService } from '@taiga-ui/core';
//import { PolymorpheusComponent } from '@tinkoff/ng-polymorpheus';
 
@Component({
  standalone: true,
  selector: 'app-root',
  styleUrls: ['./app.component.less'],
  templateUrl: './app.component.html',
  imports: [
    TuiRootModule,
    CandidateContentModule,
    CandidateTitleModule,
    CandidateButtonModule,
    TuiButtonModule,
    TuiDialogModule,
    CandidateBoardComponent,

    // ...
  ],
})
export class AppComponent {

  constructor(
    @Inject(Injector) private readonly injector: Injector
  ) {}
}