import { Navigate, useNavigate } from "react-router-dom";
import { ExpandedSidenavProps } from "../../../interfaces/expandedsidenavprops";



export default function ExpandedSideNav(props: ExpandedSidenavProps)
{
    const navigate = useNavigate();

    return (
        <div className="lg:hidden block w-auto h-full px-6 shadow-lg bg-green-300 text-center space-y-4 py-10">
            <button className="bg-green-600 rounded-lg py-4 px-2" onClick={() => props.toggleExpanded()}>Close</button>
            {props.navigations.map((navigation, index) => (
                <div key={index} onClick={() => navigate(navigation.link)}>
                    {navigation.name}
                </div>
            ))}
        </div>
    );
}