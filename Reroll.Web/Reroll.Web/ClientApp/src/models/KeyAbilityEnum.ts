
     export enum KeyAbilityEnum { 
    str = 0,
    dex = 1,
    con = 2,
    int = 3,
    wis = 4,
    cha = 5
}
export namespace KeyAbilityEnum {

       export function values() {
         return Object.keys(KeyAbilityEnum).filter(
           (type) => isNaN(<any>type) && type !== 'values'
         );
       }
     }
