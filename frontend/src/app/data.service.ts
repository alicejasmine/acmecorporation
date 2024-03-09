import { Injectable } from "@angular/core";
import { DrawEntry } from "./models";

@Injectable({
  providedIn: 'root'
})

export class DataService {
  public drawEntries: DrawEntry[]=[];
  public currentDrawEntry: DrawEntry = {};
  
}