import {NgModule} from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {TuiRootModule, TuiButtonModule, TuiDialogModule, TuiErrorModule, TuiDataListModule, TuiLoaderModule} from '@taiga-ui/core';
import {AppComponent} from './app.component';
import {TuiCardModule, TuiSurfaceModule, TuiHeaderModule, TuiIconModule, TuiChipModule} from '@taiga-ui/experimental';
import {TuiPlatformModule} from '@taiga-ui/cdk';
import {TuiDataListWrapperModule,TuiMultiSelectModule } from '@taiga-ui/kit';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule, NgTemplateOutlet } from '@angular/common';
import {TuiInputModule, TuiFieldErrorPipeModule, TuiSelectModule, TuiInputDateRangeModule} from '@taiga-ui/kit';
import { FormsModule,ReactiveFormsModule  } from '@angular/forms';
import {TUI_DIALOG_CLOSES_ON_BACK} from '@taiga-ui/cdk';
import {of} from 'rxjs';
import { JobService } from './components/services/job.service';
import { HttpClientModule } from '@angular/common/http';
import { DragDropModule } from '@angular/cdk/drag-drop';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule, // Required by Taiga UI
    TuiRootModule, // Has to go after BrowserAnimationsModule
    TuiHeaderModule,
    TuiCardModule,
    TuiSurfaceModule,
    TuiPlatformModule,
    TuiButtonModule,
    BrowserModule,
    CommonModule,
    NgTemplateOutlet,
    TuiDialogModule,
    TuiInputModule,
    FormsModule,
    ReactiveFormsModule,
    TuiErrorModule,
    TuiFieldErrorPipeModule,
    TuiDataListModule,
    TuiDataListWrapperModule,
    TuiSelectModule,
    HttpClientModule,
    TuiIconModule,
    DragDropModule,
    TuiChipModule,
    TuiLoaderModule,
    TuiMultiSelectModule,
    TuiInputDateRangeModule,
    TuiIconModule,
    // ...
  ],
  bootstrap: [AppComponent],
  providers :[
    {
      provide: TUI_DIALOG_CLOSES_ON_BACK,
      useValue: of(true),
    },
    {provide: JobService, useClass : JobService},
    HttpClientModule
  ],
})
export class AppModule {}
