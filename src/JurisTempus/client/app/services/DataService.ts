import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class DataService {

  _baseUrl: string = "/"; 

  saveTimesheet(timeBill) {
    return true;
  }

}
