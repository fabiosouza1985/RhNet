import { Component, OnInit, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Property } from 'src/app/components/models/shared/property.model';
import { Variables } from 'src/app/components/variables';

@Component({
  selector: 'app-editable-table',
  templateUrl: './editable-table.component.html',
  styleUrls: ['./editable-table.component.css']
})
export class EditableTableComponent implements OnInit {

    @Input() ds: MatTableDataSource<any>;
    @Input() properties: Property[] ;
    @Input() displayedColumns: string[];

    constructor(private variable: Variables) { }

  ngOnInit(): void {
  }

    getMaxLength(propertyName: string): number {
        var maxLength = 0;

        for (var i = 0; i < this.properties.length; i++) {
            if (this.properties[i].name === propertyName) {
                maxLength = this.properties[i].maximum;
                break;
            }
        }

        return maxLength;
    }
}
