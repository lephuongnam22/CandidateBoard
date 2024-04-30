import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { TuiTilesModule } from '@taiga-ui/kit';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'candidate-title',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    styleUrls: ['./candidate.title.less'],
    template: `	<tui-tiles 
        class="tiles"
        [debounce]="500"
        [(order)]="order">

        <tui-tile
            *ngFor="let item of columns; let index = index"
            class="tile"
            [style.order]="order.get('item.status')"
        >
            
            <h2 tuiTitle>{{item.title}}</h2>
        </tui-tile>
    </tui-tiles>`,
    imports: [
        TuiTilesModule,
        CommonModule,
    
        // ...
      ]
})

export class CandidateTitleModule {
    protected columns = [
        {title: "Applied"},
        {title: "Interview"},
        {title: "Offered"},
        {title: "Hired"}
    ];

    order = new Map();
}