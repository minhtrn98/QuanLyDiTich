'use client';

import { Input, InputProps } from '../ui/input';
import { FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage } from '../ui/form';
import { Control, FieldValues, Path } from 'react-hook-form';

interface FormInputProps<T extends FieldValues> extends InputProps {
  control: Control<T>;
  label?: string;
  name: Path<T>;
  description?: string;
}

export const FormInput = <T extends object>(props: FormInputProps<T>) => {
  const { control, name, label, description, ...rest } = props;
  return (
    <FormField
      control={control}
      name={name}
      render={({ field }) => (
        <FormItem>
          {label && <FormLabel htmlFor={name}>{label}</FormLabel>}
          <FormControl>
            <Input {...rest} {...field} />
          </FormControl>
          {description && <FormDescription>{description}</FormDescription>}
          <FormMessage />
        </FormItem>
      )}
    />
  );
};
