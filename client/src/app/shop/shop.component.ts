import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IBrand } from "./../shared/models/brand";
import { IType } from '../shared/models/productType';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  brands: IBrand[];
  types: IType[];

  constructor(private shopService: ShopService) { }

  ngOnInit() {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts().subscribe({
      next: response => {
        this.products = response.data;
        console.log(this.products);

      },
      error: error => {
        console.log(error)
      }
    })
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: response => {
        this.brands = response;
      },
      error: err => {
        console.log(err);
      }
    })
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: response => {
        this.types = response;
      },
      error: err => {
        console.log(err);
      }
    })
  }

}