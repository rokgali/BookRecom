import { Outlet } from "react-router-dom";
import { NavigationPage } from "../../interfaces/navigation_page";
import ExpandableSidenav from "./expandable_sidenav";
import TopNavigation from "./top_navigation";
import { Home, Settings, User, PersonStandingIcon, Book, ChevronFirst, ChevronLast, MoreHorizontal } from "lucide-react"
interface LayoutProps {
}

export default function Layout(props: LayoutProps)
{
    const navigations: NavigationPage[] = 
    [{icon: Home, name: 'Home', link: '/'}, 
    {icon: PersonStandingIcon, name: 'Edit Authors', link: '/author_editing'},
    {icon: Book, name: 'Saved books', link: '/cached_books'}]
    // {icon: Settings, name: 'Books by author key', link: '/book_by_author_key'}]

    return (
        <>
            <div className="w-full">
                <div className="lg:block hidden">
                    <TopNavigation navigations={navigations} />
                </div>
                <div className="flex">
                    <div className="flex-1/6 block lg:hidden">
                        <ExpandableSidenav navigations={navigations} />
                    </div>

                    <main className="min-h-full w-full lg:min-h-5/6 ml-24 lg:ml-0 flex-5/6 lg:mt-16 lg:px-20">
                        <Outlet />
                    </main>
                </div>
            </div>
        </>
    );
}