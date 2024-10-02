import { Navigate, useNavigate } from "react-router-dom";
import { NavigationPage } from "../../interfaces/navigation_page";

interface SideNavProps 
{
    navigations: NavigationPage[]
}

export default function SideNav(props: SideNavProps)
{
    const navigate = useNavigate();

    return (
        <div className="lg:hidden block w-1/6 h-auto shadow-lg bg-green-300 text-center space-y-4 py-10">
            {props.navigations.map((navigation, index) => (
                <div key={index} onClick={() => navigate(navigation.link)}>
                    {navigation.name}
                </div>
            ))}
        </div>
    );
}