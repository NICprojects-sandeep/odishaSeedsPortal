import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {
  sumprebookedquanitybyfarmer: any
  transform(items: any, searchText: string): any {
    if (!items) return [];
    if (!searchText) return items;
    console.log(items[0].prebookedquanitybyfarmer);

    searchText = searchText.toLowerCase();
    // filters the items based on the 'name' and 'company' properties
    // Thus searching for records who's name or company match the 'searchText' string
    return items.filter((item: any) => (item.District_Name.toLowerCase().includes(searchText)) ||
      (item.Crop_Name.toLowerCase().includes(searchText) || (item.prebookedquanitybyfarmer) == 0)
      // item.filter((item1:any) =>item1.prebookedquanitybyfarmer)
      // .map((item2:any) => +item2.prebookedquanitybyfarmer)
      // .reduce((sum:any, current:any) => sum + current))      
    );
    console.log(items);

  }

}