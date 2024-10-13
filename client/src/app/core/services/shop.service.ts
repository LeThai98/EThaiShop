import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Product } from '../../shared/models/product';
import { Pagination } from '../../shared/models/pagination';
import { H } from '@angular/cdk/keycodes';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api';
  private http = inject(HttpClient);
  types: string[] = [];
  brands: string[] = [];

  getProduct(brands?: string[], types?: string[]) {
    let params = new HttpParams();
    if (brands && brands.length > 0) {
      params = params.append('brands', brands.join(','));
    }
    if (types && types.length > 0) {
      params = params.append('types', types.join(','));
    }
    params = params.append('pageSize', '20');

    return this.http.get<Pagination<Product>>(this.baseUrl + '/product', { params });
  }

  getBrands() {
    if (this.brands.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + '/product/brands').subscribe({
      next: response => this.brands = response,
    })
  }

  getTypes() {
    if (this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + '/product/types').subscribe({
      next: response => this.types = response,
    })
  }

}
