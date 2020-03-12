import { Component, OnInit } from '@angular/core';
import { DataService } from './services/DataService';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'theApp',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
}) 
export class AppComponent implements OnInit {
  theForm: FormGroup;
  employee: string = "Shawn Wildermuth";
  employeeId: number = 1;
  message: string = "";
  cases = [
    {
      id: 1,
      fileNumber: "12345",
      client: "Jones, M."
    },
    {
      id: 2,
      fileNumber: "1235",
      client: "Smith, J."
    },
  ];

  constructor(private _formBldr: FormBuilder, private _dataService: DataService) {
  }

  ngOnInit(): void {
    this.theForm = this._formBldr.group({
      caseId: [""],
      workDate: [new Date()],
      timeSegments: [0],
      rate: [120.00],
      workDescription: [""]
    });
  }




  save() {
    let timeBill = this.theForm.value;
    timeBill.employeeId = this.employeeId;
    this._dataService.saveTimesheet(timeBill);
    this.message = "Saved...";
  }
}
