import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss'],
})
export class ServerErrorComponent implements OnInit {
  error: any;

  constructor(private router: Router) {
    const nav = this.router.getCurrentNavigation();
    this.error = nav?.extras?.state?.console.error();
  }

  ngOnInit(): void {}
}
