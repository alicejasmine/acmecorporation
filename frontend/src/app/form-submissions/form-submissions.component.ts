import {Component} from '@angular/core';
import {firstValueFrom} from 'rxjs';
import {HttpClient} from "@angular/common/http";
import {DrawEntry} from '../models';
import {environment} from 'environment';
import {DataService} from '../data.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-form-submissions',
  templateUrl: './form-submissions.component.html',
  styleUrls: ['./form-submissions.component.css']
})
export class FormSubmissionsComponent {
  resultsPerPage: number = 10;
  currentPage: number = 1;


  constructor(public dataService: DataService,
              public http: HttpClient, public route: ActivatedRoute,
              public router: Router) {
    this.getEntries();
  }

  async getEntries() {
    const QueryParams = await firstValueFrom(this.route.queryParams);
    const page = QueryParams['page'] ?? 1
    const resultsPerPage = QueryParams['resultsPerPage'] ?? 10;
    this.currentPage = Number.parseInt(page) ?? 1;
    this.resultsPerPage = Number.parseInt(resultsPerPage) ?? 10;
    this.dataService.drawEntries = await firstValueFrom<DrawEntry[]>(this.http.get<DrawEntry[]>(
      environment.baseUrl + "/entries?page=" + this.currentPage + "&resultsPerPage=" + this.resultsPerPage));
  }


  async NextPage() {
    if (this.currentPage >= 1 && this.resultsPerPage >= 1) {
      this.currentPage = this.currentPage + 1;
      await this.router.navigate(['/entries'], {
        queryParams: {
          page: this.currentPage,
        }
      });
      this.getEntries();
    }
  }


  async PreviousPage() {
    if (this.currentPage > 1) {
      this.currentPage = this.currentPage - 1;
      await this.router.navigate(['entries'], {
        queryParams: {
          page: this.currentPage,
        }
      });
      this.getEntries();
    }
  }
}

