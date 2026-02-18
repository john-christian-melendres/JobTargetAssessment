export type IUser = {
  id: number;
  name: string;
  userName: string;
  phone: string;
  website: string;
  email: string,
  address?: {
    street: string;
    suite: string;
    city: string;
    geo: {
      lat: string;
      lng: string;
    };
  };
  company?: {
    name: string;
    catchPhrase: string;
    bs: string;
  };
};
