import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Skinet Title';
  products: any[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe({
      next: (response: any) => {
        this.products = response.data;
        console.log(response)
      },
      error: (error) => console.log(error),
    }
    );
    // this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe(
    //   (response: any) =>
    //     console.log(response)
    //   , error => {
    //     console.log(error);

    //   }
    // );
  }


}
