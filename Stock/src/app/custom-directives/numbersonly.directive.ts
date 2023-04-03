import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appNumbersonly]'
})
export class NumbersOnlyDirective {

  private specialKeys: Array<string> = ['Backspace', 'Tab', 'End', 'Home', 'ArrowLeft', 'ArrowRight', 'Del', 'Delete', 'Enter'];

  constructor(private _el: ElementRef) { }

  @HostListener('input', ['$event']) onInputChange(event: any) {
    console.log('hiiii');
    
    if (this.specialKeys.indexOf(event.key) !== -1) { return; }
    const initialValue2 = this._el.nativeElement.value;
    const initialValue1 = initialValue2.replace(/^[00]+$/g, '0');
    if ((/^[0][1-9]+$/g).test(initialValue1)) {
      const initialValue = initialValue1.replace(/^0+/g, '');
      this._el.nativeElement.value = initialValue.replace(/[^\d]+$/g, '');
      if (initialValue !== this._el.nativeElement.value) {
        event.stopPropagation();
      }
    } else {
      this._el.nativeElement.value = initialValue1.replace(/[^\d]+$/g, '');
      if (initialValue1 !== this._el.nativeElement.value) {
        event.stopPropagation();
      }
    }
  }

}
