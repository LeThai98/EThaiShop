import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api';
  private http = inject(HttpClient);
  products: Product[] = [];
  title = 'EThaiShop'; 
  constructor() {}
  ngOnInit() {
    this.http.get<Pagination<Product>>(this.baseUrl + '/product').subscribe({
      next: response => {
        this.products =  response.data;
        console.log(this.products);
      },
      error: error => {
        console.error('There was an error!', error);
      },
      complete: () => {
        
      }
    });
  }
  
}
