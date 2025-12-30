import { ChangeDetectorRef,Component, OnInit} from '@angular/core'; // ChangeDetectorRef ekledik
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';


// Veri kalıbı (Interface)
interface Customer {
  id: number;
  firstName: string;
  lastName: string;
}

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [CommonModule, HttpClientModule], 
templateUrl: './customer.html' // Tekrar HTML dosyasına yönlendirdik
})
export class CustomerComponent implements OnInit {
  public customers: any[] = [];


  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

ngOnInit() {
  this.http.get<any[]>('https://localhost:7150/api/customers')
      .subscribe({
        next: (result) => {
          console.log("Veri geldi:", result);
          this.customers = result;
          this.cdr.detectChanges(); // Angular'a "Hadi ekranı güncelle" dedik
        },
        error: (err) => console.error("Hata:", err)
      });
  }
}

