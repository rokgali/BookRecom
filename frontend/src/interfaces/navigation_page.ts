import { LucideProps } from "lucide-react";

export interface NavigationPage {
    icon: React.ForwardRefExoticComponent<Omit<LucideProps, "ref"> & React.RefAttributes<SVGSVGElement>>,
    name: string,
    link: string
}