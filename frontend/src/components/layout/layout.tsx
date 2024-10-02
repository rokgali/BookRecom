import SideNav from "./sidenav";
import { Outlet } from "react-router-dom";
import TopNav from "./topnav";
import { NavigationPage } from "../../interfaces/navigation_page";

export default function Layout()
{
    const pages: NavigationPage[] = [{name: 'Home', link: '/'}, {name: 'Edit Authors', link: '/author_editing'}]

    return (
        <>
            <div>
                <TopNav navigations={pages} />
            </div>
            <div className="flex h-full">
                <SideNav navigations={pages} />

                <main className="lg:h-5/6 w-full">
                    <Outlet />
                </main>
            </div>
        </>
    );
}