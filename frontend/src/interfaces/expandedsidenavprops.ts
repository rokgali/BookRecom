import { NavigationPage } from "./navigation_page";

export interface ExpandedSidenavProps 
{
    navigations: NavigationPage[],
    toggleExpanded(): void
}