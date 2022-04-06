
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IBrand } from "./../shared/models/brand";
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  // @ViewChild('search', { static: true }) searchTerm: ElementRef;
  @ViewChild('search') searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams = new ShopParams();
  totalCount: number;

  // brandIdSelected: number = 0;
  // typeIdSelected: number = 0;
  // sortSelected = 'name';

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit() {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => {
        // console.log('getproducts type: ', typeof response);

        this.products = response.data;

        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
        // console.log(this.products);

      },
      error: error => {
        console.log(error)
      }
    })
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: response => {
        this.brands = [{ id: 0, name: 'All' }, ...response];
      },
      error: err => {
        console.log(err);
      }
    })
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: response => {
        this.types = [{ id: 0, name: 'All' }, ...response];
      },
      error: err => {
        console.log(err);
      }
    })
  }

  onBrandSelected(brandId: number) {
    // console.log('brandId: ', brandId);
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    // console.log('typeId: ', typeId);
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    console.log('onpagechanged event: ', event);

    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }

}
