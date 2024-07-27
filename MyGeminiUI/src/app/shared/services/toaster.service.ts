import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ToasterService {

  constructor(private messageService: MessageService) { }

  showSuccess(title: string, detail: string, duration?: number) {
    this.messageService.add({ severity: 'success', summary: title, detail: detail, life: duration === undefined ? 3000 : duration });
  }

  showError(title: string, detail: string, duration?: number) {
    this.messageService.add({ severity: 'error', summary: title, detail: detail, life: duration === undefined ? 5000 : duration });
  }

  showInfo(title: string, detail: string, duration?: number) {
    this.messageService.add({ severity: 'info', summary: title, detail: detail, life: duration === undefined ? 3000 : duration });
  }

  showWarn(title: string, detail: string, duration?: number) {
    this.messageService.add({ severity: 'warn', summary: title, detail: detail, life: duration === undefined ? 3000 : duration });
  }
}
