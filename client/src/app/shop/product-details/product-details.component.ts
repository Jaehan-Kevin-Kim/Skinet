import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faMinusCircle, faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { IProduct } from '../../shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute) { }
  faPlusCircle = faPlusCircle
  faMinusCircle = faMinusCircle

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.shopService.getProduct(id).subscribe(
      {
        next: (product: IProduct) => {
          this.product = product;
        },
        error: error => console.log(error)
      }
    )
  }
}
