import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/products';
import { ShopParams } from '../../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  types: string[] = [];
  brands: string[] = [];

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if(shopParams.brands.length > 0){
       params = params.set('brands', shopParams.brands.join(','));
    }
    if(shopParams.types.length > 0){ 
      params = params.set('types', shopParams.types.join(','));
    }

    if(shopParams.sort){
      params = params.set('sort', shopParams.sort);
    } 

    params = params.set('pageSize', shopParams.pageSize);
    params = params.set('pageNumber', shopParams.pageNumber);
    return this.http.get<Pagination<Product>>(this.baseUrl + 'products', {params});
  }

  getBrands() {
    if (this.brands.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'products/brands')
               .subscribe({
                 next: response => this.brands = response,
                 error: error => console.log(error)
               });
  }
  getTypes() {
    if (this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'products/types')
               .subscribe({
                 next: response => this.types = response,
                 error: error => console.log(error)
               });
  }

}
